from .database_service import DatabaseService
from datetime import datetime
from ..mangas_service import MangasService

class HistoriesDatabaseService:
    @staticmethod
    def create_histories_table() -> None:
        query = '''CREATE TABLE IF NOT EXISTS histories (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            user_id INTEGER NOT NULL,
            manga_id TEXT NOT NULL,
            chapter_number INTEGER NOT NULL,
            page_number INTEGER NOT NULL,
            read_date INTEGER NOT NULL,
            FOREIGN KEY (user_id) REFERENCES users(id)
        );'''
        with DatabaseService() as db_service:
            db_service.execute_query(query)

    @staticmethod
    def add_to_history(user_id: int, manga_id: str, chapter_number: int, page_number: int) -> None:
        query = "INSERT INTO histories (user_id, manga_id, chapter_number, page_number, read_date) VALUES (?, ?, ?, ?, ?)"
        values = (user_id, manga_id, chapter_number, page_number, int(datetime.now().timestamp()))
        with DatabaseService() as db_service:
            db_service.execute_query(query, values)
    
    @staticmethod
    def get_manga_history(user_id: int) -> list:
        query = "SELECT manga_id, MAX(read_date) AS latest_read_date FROM histories WHERE user_id = ? GROUP BY manga_id ORDER BY latest_read_date DESC;" 
        values = (user_id,)
        with DatabaseService() as db_service:
            rows = db_service.select_query(query, values)
        return [MangasService.get_manga_info(manga_id[0]) for manga_id in rows]
    
    @staticmethod
    def get_last_page_read(user_id: int, manga_id: str):
        query = "SELECT DISTINCT chapter_number, page_number FROM histories WHERE user_id = ? AND manga_id = ? ORDER BY read_date DESC LIMIT 1";
        values = (user_id, manga_id)

        with DatabaseService() as db_service:
            row = db_service.select_one_query(query, values)
        bookmark = row if row is not None else (0,0)
        return { 'chapterNumber': bookmark[0], 'pageNumber': bookmark[1] }
