﻿using MediatR;

namespace Account.Application.Commands.ChangeEmail;

public record ChangeEmailRequest(string CurrentEmail, string NewEmail) : IRequest<ChangeEmailResponse>;