using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Dtos;
using Library_Rest_API.Services;
using Library_Rest_API.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTest.Services
{
    [TestClass]
    public class GetAllLibrariesServiceTest
    {
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly ILibraryFactory _libraryFactory;
        private readonly GetAllLibrariesService _getAllLibrariesService;

        public GetAllLibrariesServiceTest()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _libraryFactory = new LibraryFactoryImpl();
            _getAllLibrariesService = new(_libraryRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllLibraries_OneItem()
        {
            // Arrange
            string libName = "lib1";
            LibraryId libraryId = new(Guid.Empty);
            LibraryName libraryName = new(libName);
            var library = _libraryFactory.CreateLibrary(libraryId, libraryName);

            IEnumerable<Library> libraries = [library];

            _libraryRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(libraries);

            // Act
            IEnumerable<LibraryDto> result = await _getAllLibrariesService.GetAllLibraries();
            IEnumerable<LibraryDto> expected = [new LibraryDto {
                Id = libraryId.Id,
                Name = libName,
            }];

            // Assert
            Xunit.Assert.Equal(expected.ToList().First().Id, result.ToList().First().Id);
            Xunit.Assert.Equal(expected.ToList().First().Name, result.ToList().First().Name);
            Xunit.Assert.Equal(expected.ToList().ToString(), result.ToList().ToString());
            _libraryRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}