using AutoMapper;
using CustomerApiMicroservice.Domain.Entities;
using CustomerApiMicroservice.Models;
using CustomerApiMicroservice.Service.Commands;
using CustomerApiMicroservice.Service.Querires;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApiMicroservice.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, IMediator mediator) {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> Customer(CreateCustomerModel createCustomerModel) {
            try {
                return await _mediator.Send(new CreateCustomerCommand {
                    Customer = _mapper.Map<Customer>(createCustomerModel)
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> Customer(UpdateCustomerModel updateCustomerModel) {
            try {
                var customer = await _mediator.Send(new GetCustomerQuery {
                    Id = updateCustomerModel.Id.ToString(),
                });

                if (customer == null) {
                    return BadRequest($"No customer found with the id {updateCustomerModel.Id}");
                }

                return await _mediator.Send(new UpdateCustomerCommand {
                    Customer = _mapper.Map(updateCustomerModel, customer)
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll() {
            try {
                return await _mediator.Send(new GetAllCustomersQuery { });
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id) {
            try {
                return await _mediator.Send(new GetCustomerQuery { Id = id });
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
