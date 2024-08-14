using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.Dtos;
using Library_Rest_API.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Library_Rest_API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly ICreateLibrary createLibraryService;
        private readonly IGetAllLibraries getAllLibrariesService;
        private readonly IGetLibraryById getLibraryByIdService;
        private readonly IUpdateLibrary updateLibraryService;
        private readonly IDeleteLibrary deleteLibraryService;

        public LibrariesController(
            ICreateLibrary createLibraryService, 
            IGetAllLibraries getAllLibrariesService, 
            IGetLibraryById getLibraryByIdService, 
            IUpdateLibrary updateLibraryService,
            IDeleteLibrary deleteLibraryService
            )
        {
            this.createLibraryService = createLibraryService;
            this.getAllLibrariesService = getAllLibrariesService;
            this.getLibraryByIdService = getLibraryByIdService;
            this.updateLibraryService = updateLibraryService;
            this.deleteLibraryService = deleteLibraryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibrariesResponse>>> GetAllLibraries()
        {
            IEnumerable<LibraryDto> libraries = await getAllLibrariesService.GetAllLibraries();

            return Ok(libraries.ToList().ConvertAll<LibrariesResponse>(libDto => new LibrariesResponse(libDto.Id, libDto.Name)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibrariesResponse>> GetLibraryById([FromRoute(Name ="id")] Guid id)
        {
            LibraryId libraryId = new(id);

            try
            {
                LibraryDto result = await getLibraryByIdService.GetLibraryById(libraryId);

                return Ok(new LibrariesResponse(result.Id, result.Name));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLibrary([FromBody] LibraryRequest libraryRequest)
        {
            LibraryId libraryId = new(Guid.Empty);
            LibraryName libraryName;
            try
            {
                libraryName = new(libraryRequest.Name);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                int result = await createLibraryService.CreateLibrary(libraryId, libraryName);

                if (result == 1)
                {
                    return CreatedAtAction(nameof(CreateLibrary), libraryRequest);
                } else 
                {
                    return Problem();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary([FromRoute(Name = "id")] Guid id, [FromBody] LibraryRequest libraryRequest)
        {
            LibraryId libraryId;
            LibraryName libraryName;
            try
            {
                libraryId = new(id);
                libraryName = new(libraryRequest.Name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                int result = await updateLibraryService.UpdateLibrary(libraryId, libraryName);

                if (result == 0)
                {
                    return Conflict();
                }

                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary([FromRoute(Name = "id")] Guid id)
        {
            LibraryId libraryId = new(id);

            try
            {
                await deleteLibraryService.DeleteLibrary(libraryId);

                return NoContent();
            }
            catch (DBConcurrencyException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
