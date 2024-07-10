from .database_service import DatabaseService

class FavoritesDatabaseService:
    @staticmethod
    def create_favorites_table() -> None:
        query = '''CREATE TABLE IF NOT EXISTS favorites (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            user_id INTEGER NOT NULL,
            manga_id TEXT NOT NULL,
            FOREIGN KEY (user_id) REFERENCES users(id)
        );'''
        with DatabaseService() as db_service:
            db_service.execute_query(query)

    @staticmethod
    def add_favorites(user_id: int, manga_id: str) -> None:
        query = "INSERT INTO favorites (user_id, manga_id) VALUES (?, ?)"
        values = (user_id, manga_id)
        with DatabaseService() as db_service:
            db_service.execute_query(query, values)
    