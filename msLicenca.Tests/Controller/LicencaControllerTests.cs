using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using msLicenca.Controllers;
using msLicenca.Dto;
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
        public readonly Mock<IMapper> _mockMapper;
        public readonly Mock<ILicencaService> _mockService;
        public readonly LicencaController _controller;
        public LicencaControllerTests()
        {
            _mockService = new Mock<ILicencaService>();
            _mockMapper = new Mock<IMapper> ();
            _controller = new LicencaController(_mockService.Object, _mockMapper.Object );
        }

        [Fact]
        public async Task GetAll_ReturnsAllLicences_WithListOfLicencaResponseDTO()
        {
            var licences = new List<Licenca>
            {
                new Licenca { Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), Status = StatusLicenca.ATIVA },
                new Licenca { Id = 2, IdEpresa = 1, IdTipoLicenca = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), Status = StatusLicenca.ATIVA }
            };

            var licencaDtos = new List<LicencaResponseDTO>
            {
                new LicencaResponseDTO { Id = 1, IdEpresa = 1, IdTipoLicenca = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), Status = StatusLicenca.ATIVA },
                new LicencaResponseDTO { Id = 2, IdEpresa = 1, IdTipoLicenca = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), Status = StatusLicenca.ATIVA }
            };

            _mockService.Setup(s => s.GetAllLicencasAsync()).ReturnsAsync(licences);
            _mockMapper.Setup(m => m.Map<List<LicencaResponseDTO>>(licences)).Returns(licencaDtos);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<LicencaResponseDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsLicenceById_WithValidId()
        {
            var licence = new Licenca
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Status = StatusLicenca.ATIVA,
                Data_emissao = DateOnly.FromDateTime(DateTime.Now),
                Data_validade = DateOnly.FromDateTime(DateTime.Now)
            };

            var licencaDto = new LicencaResponseDTO
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Status = StatusLicenca.ATIVA,
                Data_emissao = DateOnly.FromDateTime(DateTime.Now),
                Data_validade = DateOnly.FromDateTime(DateTime.Now)
            };

            _mockService.Setup(s => s.GetLicencaByIdAsync(1)).ReturnsAsync(licence);
            _mockMapper.Setup(m => m.Map<LicencaResponseDTO>(licence)).Returns(licencaDto);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<LicencaResponseDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task CreateLicence_ReturnsLicenceCreated_WithSuccessStatusCode()
        {
            var licencaRequestDto = new LicencaRequestDTO
            {
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = DateOnly.FromDateTime(DateTime.Now),
                Data_validade = DateOnly.FromDateTime(DateTime.Now),
                Status = StatusLicenca.ATIVA
            };

            var licencaEntity = new Licenca
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = licencaRequestDto.Data_emissao,
                Data_validade = licencaRequestDto.Data_validade,
                Status = licencaRequestDto.Status
            };

            var licencaResponseDto = new LicencaResponseDTO
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = licencaRequestDto.Data_emissao,
                Data_validade = licencaRequestDto.Data_validade,
                Status = licencaRequestDto.Status
            };

            _mockMapper.Setup(m => m.Map<Licenca>(licencaRequestDto)).Returns(licencaEntity);
            _mockService.Setup(s => s.CreateLicencaAsync(licencaEntity)).ReturnsAsync(licencaEntity);
            _mockMapper.Setup(m => m.Map<LicencaResponseDTO>(licencaEntity)).Returns(licencaResponseDto);

            var result = await _controller.Create(licencaRequestDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<LicencaResponseDTO>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task UpdateLicence_ReturnsUpdatedLicence_WithSuccessStatus()
        {
            var licencaRequestDto = new LicencaRequestDTO
            {
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = DateOnly.FromDateTime(DateTime.Now),
                Data_validade = DateOnly.FromDateTime(DateTime.Now),
                Status = StatusLicenca.ATIVA
            };

            var existingEntity = new Licenca
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = licencaRequestDto.Data_emissao,
                Data_validade = licencaRequestDto.Data_validade,
                Status = licencaRequestDto.Status
            };

            var updatedEntity = new Licenca
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = licencaRequestDto.Data_emissao,
                Data_validade = licencaRequestDto.Data_validade,
                Status = licencaRequestDto.Status
            };

            var updatedDto = new LicencaResponseDTO
            {
                Id = 1,
                IdEpresa = 1,
                IdTipoLicenca = 1,
                Data_emissao = licencaRequestDto.Data_emissao,
                Data_validade = licencaRequestDto.Data_validade,
                Status = licencaRequestDto.Status
            };

            _mockService.Setup(s => s.GetLicencaByIdAsync(1)).ReturnsAsync(existingEntity);
            _mockMapper.Setup(m => m.Map(licencaRequestDto, existingEntity)).Returns(updatedEntity);
            _mockService.Setup(s => s.UpdateLicencaAsync(1, updatedEntity)).ReturnsAsync(updatedEntity);
            _mockMapper.Setup(m => m.Map<LicencaResponseDTO>(updatedEntity)).Returns(updatedDto);

            var result = await _controller.Update(1, licencaRequestDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<LicencaResponseDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task DeleteLicence_ReturnsNoContent_WhenDeleted()
        {
            _mockService.Setup(s => s.DeleteLicencaAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteLicence_ReturnsNotFound_WhenNotDeleted()
        {
            _mockService.Setup(s => s.DeleteLicencaAsync(1)).ReturnsAsync(false);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
