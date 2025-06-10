using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using msLicenca.Controllers;
using msLicenca.Entity;
using msLicenca.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msLicenca.Tests.Controller
{
    public class LicencaControllerTests
    {
        public readonly Mock<ILicencaService> _mockService;
        public readonly LicencaController _controller;
        public LicencaControllerTests()
        {
            _mockService = new Mock<ILicencaService>();
            _controller = new LicencaController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllLicences_WithListOfLicences() 
        {
            var licence = new List<Licenca>
            {
                new Licenca{Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), Status = StatusLicenca.ATIVA },
                new Licenca{Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), Status = StatusLicenca.ATIVA }

            };

            _mockService.Setup(s => s.GetAllLicencasAsync()).ReturnsAsync(licence);
            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Licenca>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
        [Fact]
        public async Task GetById_ReturnsLicenceById_WithValidId() 
        {
            var licence = new Licenca { Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Status = StatusLicenca.ATIVA, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now) };
            _mockService.Setup(s => s.GetLicencaByIdAsync(1)).ReturnsAsync(licence) ;

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Licenca>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            
        }

        [Fact]
        public async Task CreateLicence_ReturnLicenceCreated_WithSuccessStatusCode() 
        {

            var licence = new Licenca { Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Status = StatusLicenca.ATIVA, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now) };
            _mockService.Setup(p => p.CreateLicencaAsync(licence)).ReturnsAsync(licence);
            var result = await _controller.Create(licence);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Licenca>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);

        }

        [Fact]
        public async Task UpdateLicence_ReturnUpdatedLicence_WithSuccessStatus() 
        {
            var licence = new Licenca { Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Status = StatusLicenca.ATIVA, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now) };
            _mockService.Setup(p => p.UpdateLicencaAsync(1, licence)).ReturnsAsync(licence);
            var result = await _controller.Update(1, licence);
            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Licenca>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);


        }


        [Fact]
        public async Task DeleteLicence_ReturnDeleted_SuccessDelete() 
        {
            _mockService.Setup(p => p.DeleteLicencaAsync(1)).ReturnsAsync(true);
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);


        }
        [Fact]
        public async Task DeleteLicence_ReturnNotFound_WhenNotFound()
        {
            _mockService.Setup(p => p.DeleteLicencaAsync(1)).ReturnsAsync(false);
            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);


        }

    }
}
