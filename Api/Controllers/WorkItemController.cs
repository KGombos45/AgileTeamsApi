using Api.Common;
using Application.Workitem.Queries.GetStatuses;
using Application.WorkItem.Commands.Create;
using Application.WorkItem.Commands.CreateComment;
using Application.WorkItem.Commands.Delete;
using Application.WorkItem.Commands.Update;
using Application.WorkItem.Queries.GetPriorities;
using Application.WorkItem.Queries.GetTypes;
using Application.WorkItem.Queries.GetUserWorkItems;
using Application.WorkItem.Queries.GetWorkItemOwnerCount;
using Application.WorkItem.Queries.GetWorkItems;
using Application.WorkItem.Queries.GetWorkItemStatusCount;
using Application.WorkItem.Queries.GetWorkItemTypeCount;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ApiControllerBase
    {
        [HttpPost]
        [Route("Create")]
        //POST: api/WorkItem/Create
        public async Task<ActionResult> Create(WorkItem workItem)
        {
            var command = new CreateCommand
            {
                WorkItem = workItem
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        //PUT : /api/WorkItem/Update
        public async Task<ActionResult> Update(WorkItem workItem)
        {
            var command = new UpdateCommand
            {
                WorkItem = workItem
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult> Delete(string workItemId)
        {
            var command = new DeleteCommand
            {
                WorkItemId = workItemId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("AddComment")]
        //POST: api/WorkItem/AddComment
        public async Task<ActionResult<WorkItem>> CreateComment(WorkItemComment workItemComment)
        {
            var command = new CreateCommentCommand
            {
                WorkItemComment = workItemComment
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("Statuses")]
        //GET : /api/WorkItem/Statuses
        public async Task<ActionResult<List<WorkItemStatus>>> GetStatuses()
        {
            var response = await Mediator.Send(new GetStatusesQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("Types")]
        //GET : /api/WorkItem/Types
        public async Task<ActionResult<List<WorkItemType>>> GetTypes()
        {
            var response = await Mediator.Send(new GetTypesQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("Priorities")]
        //GET : /api/WorkItem/Priorities
        public async Task<ActionResult<List<WorkItemPriority>>> GetPriorities()
        {
            var response = await Mediator.Send(new GetPrioritiesQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("WorkItems")]
        //GET : /api/WorkItem/WorkItems
        public async Task<ActionResult<List<WorkItem>>> GetWorkItems()
        {
            var response = await Mediator.Send(new GetWorkItemsQuery());

            return Ok(response);
        }


        [HttpGet]
        [Route("GetWorkItemStatusCount")]
        //GET : /api/WorkItem/GetWorkItemStatusCounts
        public async Task<ActionResult<List<Array>>> GetWorkItemStatusCount()
        {
            var response = await Mediator.Send(new GetWorkItemStatusCountQuery());

            return Ok(response);
        }


        [HttpGet]
        [Route("GetWorkItemPriorityCount")]
        //GET : /api/WorkItem/GetWorkItemPriorityCounts
        public async Task<ActionResult<List<Array>>> GetWorkItemPriorityCount()
        {
            var response = await Mediator.Send(new GetWorkItemPriorityCountQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("GetWorkItemOwnerCount")]
        //GET : /api/WorkItem/GetWorkItemOwnerCounts
        public async Task<ActionResult<List<Array>>> GetWorkItemOwnerCount()
        {
            var response = await Mediator.Send(new GetWorkItemOwnerCountQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("WorkItems/{userId}")]
        //GET : /api/WorkItem/WorkItems
        public async Task<ActionResult<List<WorkItem>>> GetUserWorkItems(string userId)
        {
            var query = new GetUserWorkItemsQuery
            {
                UserId = userId
            };

            var response = await Mediator.Send(query);

            return Ok(response);
        }
    }
}
