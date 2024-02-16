using Microsoft.AspNetCore.Mvc;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(Storage.Books);
    }

    [HttpGet("{id}")]
    public IActionResult GetBook(int id)
    {
        var book = Storage.Books.Find(b => b.Id == id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book book)
    {
        book.Id = Storage.Books.Count + 1;
        Storage.Books.Add(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book bookUpdate)
    {
        var book = Storage.Books.Find(b => b.Id == id);
        if (book == null) return NotFound();
        book.Title = bookUpdate.Title;
        book.Author = bookUpdate.Author;
        book.IsAvailable = bookUpdate.IsAvailable;
        return Ok(book);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = Storage.Books.Find(b => b.Id == id);
        if (book == null) return NotFound();
        Storage.Books.Remove(book);
        return NoContent();
    }
}