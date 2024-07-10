from .database_service import DatabaseService
from datetime import datetime

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
        query = "SELECT DISTINCT manga_id FROM histories WHERE user_id = ? ORDER BY read_date DESC;" 
        values = (user_id,)
        with DatabaseService() as db_service:
            rows = db_service.select_query(query, values)
        return [manga_id for manga_id in rows]
