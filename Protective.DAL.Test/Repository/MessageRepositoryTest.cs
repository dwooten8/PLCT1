using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Protective.Core.Entity;
using Protective.Core.Interfaces.Repository;
using Protective.DAL.Repository;

namespace Protective.DAL.Test.Repository
{
    [TestFixture]
    public class MessageRepositoryTest
    {
        private IRepository<Message> _repository;

        #region Setup

        [TestFixtureSetUp]
        public void Setup()
        {
            _repository = new MessageRepository();
        }

        #endregion

        #region Get List Tests

        [Test]
        public void GetList_Should_Return_A_List_of_Message_Entities()
        {
            List<Message> list = _repository.GetList();
            Assert.That(list.Count, Is.GreaterThan(0));
            Assert.That(list[0].Id, Is.GreaterThan(0));
        }
        #endregion

        #region Get Entity Tests

        [Test]
        public void GetEntity_Should_Return_a_Message_by_Message_Id()
        {
            List<Message> list = _repository.GetList();
            Message message = _repository.GetEntity(list[0]);
            Assert.That(message.Id, Is.GreaterThan(0));
        }
        #endregion

        #region Update Tests

        [Test]
        public void Update_Should_Update_a_Message_IsStarred_Value()
        {
            List<Message> list = _repository.GetList();
            Message message = new Message()
            {
                Id = list[0].Id,
                IsStarred = true
            };

            bool success = _repository.Update(message);
            Assert.That(success, Is.EqualTo(true));
        }
        #endregion

        #region Create Tests

        [Test]
        public void Create_Should_Create_a_Message()
        {
            Message message = new Message()
            {
                MessageText = "Test Message Text",
                IsStarred = false,
                AddedDate = DateTime.Now
            };

            message = _repository.Create(message);
            Assert.That(message.Id, Is.GreaterThan(0));
        }

        #endregion

        #region Delete Tests

        [Test]
        public void Delete_Should_Delete_a_Message_by_Id()
        {
            List<Message> list = _repository.GetList();
            bool success = _repository.Delete(list[0]);
            Assert.That(success, Is.EqualTo(true));
        }
        #endregion

    }
}
