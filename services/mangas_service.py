import os
from config import DATA_PATH

class MangasService:
    @staticmethod
    def getMiniaturePath(manga_id: str) -> str:
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
    def getPage(manga_id: str, chapter: int, page: int) -> str:
        chapters_folder = os.path.join(DATA_PATH, str(manga_id), "chapters")
        chapters = os.listdir(chapters_folder)

        pages_folder = os.path.join(chapters_folder, chapters[chapter])
        pages = os.listdir(pages_folder)

        return os.path.join(pages_folder, pages[page])
