from .database_service import DatabaseService
from model.manga import Manga

class MangasDatabaseService:
    @staticmethod
    def create_mangas_table() -> None:
        query = '''CREATE TABLE IF NOT EXISTS mangas (
            id string PRIMARY KEY,
            title TEXT NOT NULL,
            description TEXT,
            available_chapters INT NOT NULL
        );'''
        with DatabaseService() as db_service:
            db_service.execute_query(query)

    @staticmethod
    def add_manga(manga: Manga) -> None:
        query = "INSERT INTO mangas (id, title, description, available_chapters) VALUES (?, ?, ?, ?);"
        values = (manga.id, manga.title, manga.description, manga.available_chapters)
        with DatabaseService() as db_service:
            db_service.execute_query(query, values)

    @staticmethod
    def get_mangas() -> list[Manga]:
        query = "SELECT * FROM mangas"
        
        with DatabaseService() as db_service:
            rows = db_service.select_query(query)
        return [Manga(*manga) for manga in rows]
    
    @staticmethod
    def get_manga(manga_id: str) -> Manga:
        query = "SELECT * FROM mangas WHERE id = ?"
        values = (manga_id,)
        with DatabaseService() as db_service:
            row = db_service.select_one_query(query, values)
        return Manga(*row) if row is not None else None
