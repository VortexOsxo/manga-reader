from dataclasses import dataclass

@dataclass
class Manga:
    id: str
    title: str
    description: str
    available_chapters: int
