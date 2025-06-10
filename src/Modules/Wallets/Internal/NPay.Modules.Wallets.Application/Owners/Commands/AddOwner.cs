using MediatR;

namespace NPay.Modules.Wallets.Application.Owners.Commands;

public record AddOwner(string Email) : IRequest;