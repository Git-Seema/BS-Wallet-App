using AccountService.Models;
using AccountService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Account> _accountsRepository;

        public AccountsController(IRepository<Account> accountRepository)
        {
            _accountsRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var accounts = await _accountsRepository.GetAllAsync();
                return Ok(accounts);
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
                var account = await _accountsRepository.GetByIdAsync(id);
                if (account == null) return NotFound();
                return Ok(account);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Account account)
        {
            try
            {
                if (account == null) return BadRequest();
                await _accountsRepository.AddAsync(account);
                return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new account record");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Account account)
        {
            try
            {
                if (id != account.Id) return BadRequest("account ID mismatch");
                var accountToUpdate = await _accountsRepository.GetByIdAsync(id);
                if (accountToUpdate == null) return NotFound("account not found");
                await _accountsRepository.UpdateAsync(account);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating account record");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var accountToDelete = await _accountsRepository.GetByIdAsync(id);
                if (accountToDelete == null) return NotFound("account not found");
                await _accountsRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting account record");
            }
        }
    }

}
