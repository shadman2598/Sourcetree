using Microsoft.AspNetCore.Mvc;

[Route("api/borrowings")]
[ApiController]
public class BorrowingController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBorrowings()
    {
        return Ok(Storage.Borrowings);
    }

    [HttpGet("{id}")]
    public IActionResult GetBorrowing(int id)
    {
        var borrowing = Storage.Borrowings.Find(b => b.Id == id);
        if (borrowing == null) return NotFound();
        return Ok(borrowing);
    }

    [HttpPost]
    public IActionResult AddBorrowing([FromBody] Borrowing borrowing)
    {
        borrowing.Id = Storage.Borrowings.Count + 1;
        Storage.Borrowings.Add(borrowing);
        return CreatedAtAction(nameof(GetBorrowing), new { id = borrowing.Id }, borrowing);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBorrowing(int id, [FromBody] Borrowing borrowingUpdate)
    {
        var borrowing = Storage.Borrowings.Find(b => b.Id == id);
        if (borrowing == null) return NotFound();
        borrowing.BookId = borrowingUpdate.BookId;
        borrowing.ReaderId = borrowingUpdate.ReaderId;
        borrowing.BorrowDate = borrowingUpdate.BorrowDate;
        borrowing.ReturnDate = borrowingUpdate.ReturnDate;
        return Ok(borrowing);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBorrowing(int id)
    {
        var borrowing = Storage.Borrowings.Find(b => b.Id == id);
        if (borrowing == null) return NotFound();
        Storage.Borrowings.Remove(borrowing);
        return NoContent();
    }
}