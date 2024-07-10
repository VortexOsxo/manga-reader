import os
from typing import List
import json
from config import DATA_PATH

class MangasService:
    @staticmethod
    def getPreviews() -> List[dict]:
        previews = []

        for folder_name in os.listdir(DATA_PATH):
            try: previews.append(MangasService.get_manga_info(folder_name))
            except: continue

        return previews
    
    @staticmethod
    def getPreview(mangaId) -> dict:
        try: return MangasService.get_manga_info(mangaId)
        except: return None

    @staticmethod
    def getMiniaturePath(manga_id: str) -> str | None :
        if manga_id in os.listdir(DATA_PATH):
            return os.path.join(DATA_PATH, manga_id, 'miniature.png')
        return None

    @staticmethod
    def getChapters(manga_id: str) -> list:
        chapters = []
        if manga_id in os.listdir(DATA_PATH):
            chapters_path = f"{DATA_PATH}/{manga_id}/chapters/"
            for file in os.listdir(chapters_path):
                chapters.append({'id': file, 'pages': len(os.listdir(f"{DATA_PATH}/{manga_id}/chapters/{file}"))})
        return chapters

    @staticmethod
    def getPage(manga_id: str, chapter: int, page: int) -> str | None:
        chapters_folder = os.path.join(DATA_PATH, str(manga_id), "chapters")
        chapters = os.listdir(chapters_folder)

        pages_folder = os.path.join(chapters_folder, chapters[chapter])
        pages = os.listdir(pages_folder)

        return os.path.join(pages_folder, pages[page])

    @staticmethod
    def get_manga_info(manga_id) -> dict:
        preview_json = open(os.path.join(DATA_PATH, manga_id, 'info'))
        preview = json.load(preview_json)
        preview['id'] = manga_id
        return preview