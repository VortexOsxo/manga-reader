from .mangas_database_service import MangasDatabaseService
from .users_database_service import UsersDatabaseService
from .favorites_database_service import FavoritesDatabaseService
from .histories_database_service import HistoriesDatabaseService

class DatabaseCreator:
    @staticmethod
    def verify_database_is_created():
        MangasDatabaseService.create_mangas_table()
        UsersDatabaseService.create_users_table()
        FavoritesDatabaseService.create_favorites_table()
        HistoriesDatabaseService.create_histories_table()
