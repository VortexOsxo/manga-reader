from .database_service import DatabaseService
from .mangas_database_service import MangasDatabaseService

class FavoritesDatabaseService:
    @staticmethod
    def create_favorites_table() -> None:
        query = '''CREATE TABLE IF NOT EXISTS favorites (
            user_id INTEGER NOT NULL,
            manga_id TEXT NOT NULL,
            FOREIGN KEY (manga_id) REFERENCES mangas(id)
            FOREIGN KEY (user_id) REFERENCES users(id)
            PRIMARY KEY (user_id, manga_id)
        );'''
        with DatabaseService() as db_service:
            db_service.execute_query(query)

    @staticmethod
    def add_favorite(user_id: int, manga_id: str) -> None:
        query = "INSERT INTO favorites (user_id, manga_id) VALUES (?, ?);"
        values = (user_id, manga_id)
        with DatabaseService() as db_service:
            db_service.execute_query(query, values)

    @staticmethod
    def delete_favorite(user_id: int, manga_id: str) -> None:
        query = "DELETE FROM favorites WHERE user_id = ? AND manga_id = ?;"
        values = (user_id, manga_id)
        with DatabaseService() as db_service:
            db_service.execute_query(query, values)

    @staticmethod
    def get_favorites(user_id: str) -> list[dict]:
        query = " SELECT manga_id from favorites WHERE user_id = ?;"
        values = (user_id,)
        with DatabaseService() as db_service:
            rows = db_service.select_query(query, values)
        return [MangasDatabaseService.get_manga(manga_id[0]) for manga_id in rows]

    @staticmethod
    def is_favorite(user_id: int, manga_id: str) -> bool:
        query = "SELECT 1 FROM favorites WHERE user_id = ? AND manga_id = ? LIMIT 1"
        values = (user_id, manga_id)
        with DatabaseService() as db_service:
            result = db_service.select_one_query(query, values)
        return result is not None