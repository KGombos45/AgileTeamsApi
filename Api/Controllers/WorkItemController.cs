using Api.Common;
using Application.Workitem.Queries.GetWorkItemStatuses;
using Application.WorkItem.Commands.CreateComment;
using Application.WorkItem.Commands.CreateWorkItem;
using Application.WorkItem.Commands.DeleteWorkItem;
using Application.WorkItem.Commands.UpdateWorkItem;
using Application.WorkItem.Queries.GetPriorities;
using Application.WorkItem.Queries.GetUserWorkItems;
using Application.WorkItem.Queries.GetWorkItemOwnerCount;
using Application.WorkItem.Queries.GetWorkItems;
using Application.WorkItem.Queries.GetWorkItemStatusCount;
using Application.WorkItem.Queries.GetWorkItemTypeCount;
using Application.WorkItem.Queries.GetWorkItemTypes;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WorkItemController : ApiControllerBase
    {
        [HttpPost("create", Name = nameof(CreateWorkItem))]
        public async Task<ActionResult> CreateWorkItem([FromBody] WorkItem workItem)
        {
            var command = new CreateWorkItemCommand
            {
                WorkItem = workItem
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("update", Name = nameof(UpdateWorkItem))]
        public async Task<ActionResult> UpdateWorkItem([FromBody] WorkItem workItem)
        {
            var command = new UpdateWorkItemCommand
            {
                WorkItem = workItem
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{workItemId}/delete", Name = nameof(DeleteWorkItem))]
        public async Task<ActionResult> DeleteWorkItem([FromRoute] string workItemId)
        {
            var command = new DeleteWorkItemCommand
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

        [HttpGet("statuses", Name = nameof(GetWorkItemStatuses))]
        public async Task<ActionResult<List<WorkItemStatus>>> GetWorkItemStatuses()
        {
            var response = await Mediator.Send(new GetWorkItemStatusesQuery());

            return Ok(response);
        }

        [HttpGet("types", Name = nameof(GetWorkItemTypes))]
        public async Task<ActionResult<List<WorkItemType>>> GetWorkItemTypes()
        {
            var response = await Mediator.Send(new GetWorkItemTypesQuery());

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
