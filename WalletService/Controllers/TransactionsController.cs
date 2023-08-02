using Microsoft.AspNetCore.Mvc;
using WalletService.Models;
using WalletService.Repositories;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository<Transaction> _transactionRepository;

        public TransactionsController(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var transactions = await _transactionRepository.GetAllAsync();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var transaction = await _transactionRepository.GetByIdAsync(id);
                if (transaction == null) return NotFound();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Transaction transaction)
        {
            try
            {
                if (transaction == null) return BadRequest();
                await _transactionRepository.AddAsync(transaction);
                return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new transaction record");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Transaction transaction)
        {
            try
            {
                if (id != transaction.Id) return BadRequest("Transaction ID mismatch");
                var transactionToUpdate = await _transactionRepository.GetByIdAsync(id);
                if (transactionToUpdate == null) return NotFound("Transaction not found");
                await _transactionRepository.UpdateAsync(transaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating transaction record");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var transactionToDelete = await _transactionRepository.GetByIdAsync(id);
                if (transactionToDelete == null) return NotFound("Transaction not found");
                await _transactionRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting transaction record");
            }
        }
    }

}
