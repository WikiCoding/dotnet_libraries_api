using Library_Rest_API.Controllers;
using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.Dtos;
using Library_Rest_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryTest.Controllers
{
    [TestClass]
    public class LibraryControllerTest
    {
        private readonly ICreateLibrary createLibraryService;
        private readonly IGetAllLibraries getAllLibrariesService;
        private readonly IGetLibraryById getLibraryByIdService;
        private readonly IUpdateLibrary updateLibraryService;
        private readonly IDeleteLibrary deleteLibraryService;
        private readonly ILibraryFactory libraryFactory;
        private readonly Mock<ILibraryRepository> libraryRepositoryMock;
        private readonly LibrariesController librariesController;

        public LibraryControllerTest()
        {
            libraryRepositoryMock = new Mock<ILibraryRepository>();
            libraryFactory = new LibraryFactoryImpl();
            createLibraryService = new CreateLibraryService(libraryFactory, libraryRepositoryMock.Object);
            getAllLibrariesService = new GetAllLibrariesService(libraryRepositoryMock.Object);
            getLibraryByIdService = new GetLibraryByIdService(libraryRepositoryMock.Object);
            updateLibraryService = new UpdateLibraryService(libraryFactory, libraryRepositoryMock.Object);
            deleteLibraryService = new DeleteLibraryService(libraryRepositoryMock.Object);
            librariesController = new LibrariesController(createLibraryService, getAllLibrariesService, getLibraryByIdService, updateLibraryService, deleteLibraryService);
        }

        [TestMethod]
        public async Task CreateLibrary_Success()
        {
            // Arrange
            Guid guid = Guid.Empty;
            string libName = "lib1";
            LibraryRequest libraryRequest = new() { Name = libName };

            libraryRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<Library>())).ReturnsAsync(1);

            // Act
            var response = await librariesController.CreateLibrary(libraryRequest);
            CreatedAtActionResult? result = response as CreatedAtActionResult;

            int statusCodeExpected = 201;
            var libObjExpected = libraryRequest;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(statusCodeExpected, result.StatusCode);

            // created library response
            var createdLibrary = result.Value as LibraryRequest;
            Xunit.Assert.NotNull(createdLibrary);
            Xunit.Assert.Equal(libObjExpected, createdLibrary);
        }

        [TestMethod]
        public async Task CreateLibrary_BadLibName()
        {
            // Arrange
            Guid guid = Guid.Empty;
            string libName = " ";
            LibraryRequest libraryRequest = new() { Name = libName };

            // Act
            var response = await librariesController.CreateLibrary(libraryRequest);
            BadRequestObjectResult? result = response as BadRequestObjectResult;

            int statusCodeExpected = 400;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(statusCodeExpected, result.StatusCode);

            // created library response
            var errorMessage = result.Value as string;
            var expectedErrorMessage = "Value cannot be null. (Parameter 'Library name cannot be null or Empty')";
            
            Xunit.Assert.NotNull(errorMessage);
            Xunit.Assert.Equal(expectedErrorMessage, errorMessage);
        }
    }
}
