using Moq;
using msLicenca.Entity;
using msLicenca.Repository;
using msLicenca.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msLicenca.Tests.Service
{
    public class LicenceServiceTests
    {
        [Fact]
        public async Task GetAllLicences_ReturnAllLicences() 
        {
            var mockRepos = new Mock<ILicencaRepository>();
            mockRepos.Setup(repo => repo.listLicencas()).ReturnsAsync(new List<Licenca>
            {
                new Licenca { Id = 1,Data_emissao=DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), IdEpresa = 1, IdTipoLicenca = 2, Status = StatusLicenca.ATIVA }

            }); 
            var service = new LicencaService(mockRepos.Object);
            var result = await service.GetAllLicencasAsync();
        
            Assert.Equal(1, result.Count);

        }

        [Fact]
        public async Task GetLicenceById_ReturnLicenceById() 
        {
            var mockRepo = new Mock<ILicencaRepository>();
            var id = 1;
            mockRepo.Setup(repo => repo.licencaById(id)).ReturnsAsync(new Licenca { Id = id });
            var service = new LicencaService(mockRepo.Object);
            var result = await service.GetLicencaByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            
        }

        [Fact]
        public async Task CreateLicenca_ReturnCreatedSuccessfull() 
        {
            var mockRepo = new Mock<ILicencaRepository>();
            var newLicence = new Licenca { Id = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now) , Data_validade = DateOnly.FromDateTime(DateTime.Now), IdEpresa = 2, IdTipoLicenca = 3, Status = StatusLicenca.ATIVA };

            mockRepo.Setup(repo => repo.criarLicenca(newLicence)).ReturnsAsync(newLicence);
            var service = new LicencaService(mockRepo.Object);
            var result = await service.CreateLicencaAsync(newLicence);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(StatusLicenca.ATIVA, result.Status);
        }

        [Fact]
        public async Task UpdateLicenca_ReturnUpdatedSuccess() 
        {
            var mockRepo = new Mock<ILicencaRepository>();
            var id = 1;
            var updateLicence = new Licenca { Id = 1, Data_emissao = DateOnly.FromDateTime(DateTime.Now), Data_validade = DateOnly.FromDateTime(DateTime.Now), IdEpresa = 2, IdTipoLicenca = 3, Status = StatusLicenca.ATIVA };
            mockRepo.Setup(repo => repo.licencaById(id)).ReturnsAsync(new Licenca { Id = id });
            mockRepo.Setup(repo => repo.atualizaLicenca(id, updateLicence)).ReturnsAsync(updateLicence);
            var service = new LicencaService(mockRepo.Object);
            var result = service.UpdateLicencaAsync(id, updateLicence);
            Assert.Equal(1, result.Id);
            Assert.NotNull(result);

        }

        [Fact]
        public async Task DeleteLicence_ReturnsDeleteSuccessfully() 
        {
            var mockRepo = new Mock<ILicencaRepository>();
            var id = 1;
            mockRepo.Setup(repo => repo.licencaById(id)).ReturnsAsync(new Licenca { Id = id });

            mockRepo.Setup(repo => repo.deleteLicencencaById(id)).ReturnsAsync(true);
            var service = new LicencaService(mockRepo.Object);
            var result = await service.DeleteLicencaAsync(id);
            Assert.True(result);


        }

    }
}
