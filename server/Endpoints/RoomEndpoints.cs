using Microsoft.AspNetCore.Mvc;
using server.Application.Contracts.Services;
using server.Application.Services.Room.Dtos.Requests.CreateRoom;
using server.Application.Services.Room.Dtos.Requests.GetRoom;
using server.Application.Services.Room.Dtos.Responses;
using server.Application.Services.RoomManager.Dtos.Requests.ConnectToRoom;
using server.Domain.Entities;

namespace server.Endpoints;

public static class RoomEndpoints
{
    public static IEndpointRouteBuilder Map(this RouteGroupBuilder builder)
    {
        const string ROOM_TAG = "Room";

        builder
            .MapGet(
                "api/rooms",
                async ([FromServices] IRoomService roomService) =>
                {
                    var rooms = await roomService.GetAllAsync();

                    return Results.Ok(rooms);
                }
            )
            .Produces<RoomDto>(StatusCodes.Status200OK)
            .WithTags(ROOM_TAG)
            .WithSummary("Get all rooms")
            .WithDescription("Get all rooms");

        builder
            .MapGet(
                "api/room",
                async (
                    [AsParameters] GetRoomRequest request,
                    [FromServices] IRoomService roomService
                ) =>
                {
                    var room = await roomService.GetRoomAsync(request);

                    return Results.Ok(room);
                }
            )
            .Produces<RoomDto>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<List<string>>(StatusCodes.Status400BadRequest)
            .WithTags(ROOM_TAG)
            .WithSummary("Get room")
            .WithDescription("Get room by id");

        builder
            .MapPost(
                "api/room",
                async (
                    [FromBody] CreateRoomRequest request,
                    [FromServices] IRoomService roomService
                ) =>
                {
                    var room = await roomService.CreateAsync(request);

                    return Results.Created("", room);
                }
            )
            .Produces<Room>(StatusCodes.Status201Created)
            .Produces<List<string>>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(ROOM_TAG)
            .WithSummary("Create room")
            .WithDescription("Create a new room");

        builder
            .MapGet(
                "api/room/connect",
                async (
                    HttpContext context,
                    [FromQuery] Guid roomId,
                    [FromQuery] Guid userId,
                    [FromServices] IRoomManager roomManager
                ) =>
                {
                    if (!context.WebSockets.IsWebSocketRequest)
                    {
                        return Results.BadRequest();
                    }

                    var socket = await context.WebSockets.AcceptWebSocketAsync();

                    var request = new ConnectToRoomRequest(roomId, userId, socket);

                    await roomManager.ConnectToRoomAsync(request);

                    return Results.Empty;
                }
            )
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(ROOM_TAG)
            .WithSummary("Connect to room")
            .WithDescription(
                @"connect user to room, after connect from
                server will send request with map and active user,
                active user will be using round other cross"
            );

        return builder;
    }
}
