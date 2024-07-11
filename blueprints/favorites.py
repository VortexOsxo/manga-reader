from flask import Blueprint, jsonify
from model.user import User
from services.database.favorites_database_service import FavoritesDatabaseService
from middlewares.user_authentication import user_authentication_middleware

favorite_bp = Blueprint('favorite', __name__)

@favorite_bp.route('/', methods=['GET'])
@user_authentication_middleware
def get_favorites(user: User):
    mangas = FavoritesDatabaseService.get_favorites(user.id)
    return jsonify(mangas), 200

@favorite_bp.route('/<string:manga_id>', methods=['GET'])
@user_authentication_middleware
def check_favorite(user: User, manga_id: str):
    is_fav = FavoritesDatabaseService.is_favorite(user.id, manga_id)
    return jsonify({"isFavorite": is_fav}), 200

@favorite_bp.route('/<string:manga_id>', methods=['POST'])
@user_authentication_middleware
def add_favorite(user: User, manga_id: str):
    FavoritesDatabaseService.add_favorite(user.id, manga_id)
    return 'Favorite Added', 204

@favorite_bp.route('/<string:manga_id>', methods=['DELETE'])
@user_authentication_middleware
def remove_favorite(user: User, manga_id: str):
    FavoritesDatabaseService.delete_favorite(user.id, manga_id)
    return 'Favorite Removed', 204
    
