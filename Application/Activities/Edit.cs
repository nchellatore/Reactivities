using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set;}
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                
                _mapper = mapper;
                _context = context;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

               // activity.Title = request.Activity.Title ?? activity.Title;
               // Instead of assigning like this, we have downloaded AutoMapper, which wll automatically maps both the variable field 
               // and database fileds.

               _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}