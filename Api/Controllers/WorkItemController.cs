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
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WorkItemController : ApiControllerBase
    {
        [HttpPost("create", Name = nameof(Create))]
        public async Task<ActionResult> Create([FromBody] WorkItem workItem)
        {
            var command = new CreateCommand
            {
                WorkItem = workItem
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("update", Name = nameof(Update))]
        public async Task<ActionResult> Update([FromBody] WorkItem workItem)
        {
            var command = new UpdateCommand
            {
                WorkItem = workItem
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{workItemId}/delete", Name = nameof(Delete))]
        public async Task<ActionResult> Delete([FromRoute] string workItemId)
        {
            var command = new DeleteCommand
            {
                WorkItemId = workItemId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("addComment", Name = nameof(CreateComment))]
        public async Task<ActionResult<WorkItem>> CreateComment([FromBody] WorkItemComment workItemComment)
        {
            var command = new CreateCommentCommand
            {
                WorkItemComment = workItemComment
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("statuses", Name = nameof(GetStatuses))]
        public async Task<ActionResult<List<WorkItemStatus>>> GetStatuses()
        {
            var response = await Mediator.Send(new GetStatusesQuery());

            return Ok(response);
        }

        [HttpGet("types", Name = nameof(GetTypes))]
        public async Task<ActionResult<List<WorkItemType>>> GetTypes()
        {
            var response = await Mediator.Send(new GetTypesQuery());

            return Ok(response);
        }

        [HttpGet("priorities", Name = nameof(GetPriorities))]
        public async Task<ActionResult<List<WorkItemPriority>>> GetPriorities()
        {
            var response = await Mediator.Send(new GetPrioritiesQuery());

            return Ok(response);
        }

        [HttpGet("workItems", Name = nameof(GetWorkItems))]
        public async Task<ActionResult<List<WorkItem>>> GetWorkItems()
        {
            var response = await Mediator.Send(new GetWorkItemsQuery());

            return Ok(response);
        }


        [HttpGet("workItemStatusCount", Name = nameof(GetWorkItemStatusCount))]
        public async Task<ActionResult<List<Array>>> GetWorkItemStatusCount()
        {
            var response = await Mediator.Send(new GetWorkItemStatusCountQuery());

            return Ok(response);
        }


        [HttpGet("workItemPriorityCount", Name = nameof(GetWorkItemPriorityCount))]
        public async Task<ActionResult<List<Array>>> GetWorkItemPriorityCount()
        {
            var response = await Mediator.Send(new GetWorkItemPriorityCountQuery());

            return Ok(response);
        }

        [HttpGet("workItemOwnerCount", Name = nameof(GetWorkItemOwnerCount))]
        public async Task<ActionResult<List<Array>>> GetWorkItemOwnerCount()
        {
            var response = await Mediator.Send(new GetWorkItemOwnerCountQuery());

            return Ok(response);
        }

        [HttpGet("{userId}/workItems", Name = nameof(GetUserWorkItems))]
        public async Task<ActionResult<List<WorkItem>>> GetUserWorkItems([FromRoute] string userId)
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
