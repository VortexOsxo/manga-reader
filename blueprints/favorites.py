from flask import Blueprint, request
from model.user import User
from services.database.favorites_database_service import FavoritesDatabaseService
from middlewares.user_authentication import user_authentication_middleware

favorite_bp = Blueprint('favorite', __name__)

@favorite_bp.route('/', methods=['POST'])
@user_authentication_middleware
def get_previews(user: User):
    manga_id = request.json.get('mangaId')
    FavoritesDatabaseService.add_favorites(user.id, manga_id)
    return 'Favorites Added', 204
