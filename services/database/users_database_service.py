from .database_service import DatabaseService
from model.user import User

class UsersDatabaseService:
    @staticmethod
    def create_users_table() -> None:
        query = '''CREATE TABLE IF NOT EXISTS users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username TEXT UNIQUE NOT NULL,
                    hashed_password TEXT NOT NULL
                )'''
        with DatabaseService() as db_service:
            db_service.execute_query(query)

    @staticmethod
    def add_user(username: str, hashed_password: bytes) -> None:
        query = "INSERT INTO users (username, hashed_password) VALUES (?, ?)"
        values = (username, hashed_password)
        with DatabaseService() as db_service:
            db_service.execute_query(query, values)
    
    @staticmethod
    def get_users() -> list[User]:
        query = "SELECT * FROM users"
        
        with DatabaseService() as db_service:
            rows = db_service.select_query(query)
        return [User(*u) for u in rows]

    @staticmethod
    def get_user_by_username(username: str) -> User:
        query = "SELECT * FROM users WHERE username = ?"
        values = (username,)
        with DatabaseService() as db_service:
            row = db_service.select_one_query(query, values)
        return User(*row) if row is not None else None
    
    @staticmethod
    def get_user_by_id(id: int) -> User:
        query = "SELECT * FROM users WHERE id = ?"
        values = (id,)
        with DatabaseService() as db_service:
            row = db_service.select_one_query(query, values)
        return User(*row)