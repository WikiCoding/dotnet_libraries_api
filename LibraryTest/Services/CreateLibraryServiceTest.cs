using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Services;
using Library_Rest_API.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryTest.Services
{
    //[Class]
    [TestClass]
    public class CreateLibraryServiceTest
    {
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly ILibraryFactory _libraryFactory;
        private readonly CreateLibraryService _createLibraryService;

        public CreateLibraryServiceTest()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _libraryFactory = new LibraryFactoryImpl();
            _createLibraryService = new(_libraryFactory, _libraryRepositoryMock.Object);
        }

        //[Fact]
        [TestMethod]
        public async Task CreateLibraryService_Success()
        {
            // Arrange
            string libName = "lib1";
            LibraryId libraryId = new(Guid.Empty);
            LibraryName libraryName = new LibraryName(libName);

            _libraryRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<Library>())).ReturnsAsync(1);

            // Act
            int rowsAffected = await _createLibraryService.CreateLibrary(libraryId, libraryName);

            // Assert
            Xunit.Assert.Equal(1, rowsAffected);
            _libraryRepositoryMock.Verify(repo => repo.SaveAsync(It.Is<Library>(lib => lib.LibraryId == libraryId && lib.LibraryName == libraryName)), Times.Once);
        }

        // This test is really not good
        [TestMethod]
        public async Task CreateLibraryService_Fail()
        {
            // Arrange
            string libName = "lib1";
            LibraryId libraryId = new(Guid.Empty);
            LibraryName libraryName = new LibraryName(libName);

            _libraryRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<Library>())).ReturnsAsync(0);

            // Act
            int rowsAffected = await _createLibraryService.CreateLibrary(libraryId, libraryName);

            // Assert
            Xunit.Assert.Equal(0, rowsAffected);
            _libraryRepositoryMock.Verify(repo => repo.SaveAsync(It.Is<Library>(lib => lib.LibraryId == libraryId && lib.LibraryName == libraryName)), Times.Once);
        }
    }
}
