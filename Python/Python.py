import asyncio
from pptx import Presentation
from pptx.util import Inches
from pptx.enum.text import MSO_AUTO_SIZE, PP_PARAGRAPH_ALIGNMENT
from pptx.util import Pt
from pptx.dml.color import RGBColor


async def create():
    await asyncio.sleep(1)
    return "Hello world!"


def run_async_function():
    loop = asyncio.get_event_loop()
    return loop.run_until_complete(create())
