# FootballLeague

An API built for the PariPlay .NET Integration Developer role. It has 3 main entities (Match, Team, Player) and related actions for them. It works as a football system, allowing you to play matches, get rankings of teams etc.

## Getting Started
To get started simply **Update-Database** via the Package-Manager Console in VS. The DB default connection in *appsettings.Development.json* is set to "." - Make changes if necessary.

## Entities
All entities include controllers with CRUD operations related to them.

**Team** - Teams consist of players. They can play matches and win, lose or draw. Each game is recorded and the teams participate in a league so they have ranks as well(depending on points, then wins)

**Player** - Players determine the strength of a team. They can be transferred(which happens through the update methods, basically) between teams, or they can be free and acquired by teams.

**Match** - Matches determine the outcome of the league.

##Architecture

I've followed the same concept that is behind the [n-tier architecture](https://en.wikipedia.org/wiki/Multitier_architecture).The basic idea is the following:

1. A user sends an HTTP request to the server.
2. Asp.Net MVC route the request to a Controller action.
3. The action’s responsibility is to handle the request and the response, nothing more.
4. Since the domain logic is not of the controller responsibility, it must be delegated to another object. That object is the Service.
5. While the Service owns the domain logic responsibility, it cannot take another one so it cannot handle the data access logic. It must delegate that to the Repository.
6. The Repository, as already stated, is responsible for the data-access. Basically, it reads/writes data from/to a data-source.