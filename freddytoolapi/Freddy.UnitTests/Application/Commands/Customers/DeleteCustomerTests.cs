﻿using Freddy.Application.Commands.Customers;
using Freddy.Persistance.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Customers
{
    public class DeleteCustomerTests
    {
        private readonly Mock<ICustomers> _customersMock;
        private DeleteCustomer _deleteCustomer;

        public DeleteCustomerTests()
        {
            _customersMock = new Mock<ICustomers>();
            _deleteCustomer = new DeleteCustomer(_customersMock.Object);
        }

        [Fact]
        public void Execute_ShouldDeleteCustomer()
        {
            var customerId = new Guid("3b50451a-05d1-4e96-a2d7-7ff1e2cca09f");

            _deleteCustomer.Handle(new DeleteCustomerCommand(customerId));

            _customersMock.Verify(cmd => cmd.Delete(customerId), Times.Once);
        }
    }
}
