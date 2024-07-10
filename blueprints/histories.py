from flask import Blueprint, request
from model.user import User
from services.database.histories_database_service import HistoriesDatabaseService
from middlewares.user_authentication import user_authentication_middleware

histoy_bp = Blueprint('history', __name__)

@histoy_bp.route('/', methods=['POST'])
@user_authentication_middleware
def get_previews(user: User):
    manga_id = request.json.get('mangaId')
    chapter_number = request.json.get('chapterNumber')
    page_number = request.json.get('pageNumber')

    HistoriesDatabaseService.add_to_history(user.id, manga_id, chapter_number, page_number)
    return 'History Updated', 204
