from flask import Blueprint, jsonify, send_file
from services.mangas_service import MangasService
from services.database.mangas_database_service import MangasDatabaseService

manga_bp = Blueprint('manga', __name__)

@manga_bp.route('/', methods=['GET'])
def get_previews():
    return jsonify(MangasDatabaseService.get_mangas())

@manga_bp.route('/<string:manga_id>', methods=['GET'])
def get_preview(manga_id: str):
    return jsonify(MangasDatabaseService.get_manga(manga_id)), 200

@manga_bp.route('/<string:manga_id>/miniatures', methods=['GET'])
def get_miniature(manga_id: str):
    path = MangasService.getMiniaturePath(manga_id)
    return send_file(path, mimetype='image/jpeg'), 200

@manga_bp.route('/<string:manga_id>/chapters', methods=['GET'])
def get_chapter(manga_id: str):
    return jsonify(MangasService.getChapters(manga_id)), 200

@manga_bp.route('<string:manga_id>/<int:chapter>/<int:page>', methods=['GET'])
def get_page(manga_id: str, chapter: int, page: int):
    return send_file(MangasService.getPage(manga_id, chapter, page), mimetype='image/jpeg'), 200
