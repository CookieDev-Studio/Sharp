module internal Operations
    open Npgsql.FSharp
    open Sharp.Domain
    
    let getConnectionString = 
        let ConnectionString = ConnectionString.GetSample()
        sprintf "Host=%s;Username=%s;Password=%s;Database=%s;sslmode=Require;Trust Server Certificate=true"
            ConnectionString.Host
            ConnectionString.Username
            ConnectionString.Password
            ConnectionString.Database
    
    let private getDB queryString = 
        getConnectionString
        |> Sql.connect
        |> Sql.query queryString

    let executeQuery parseFunction =
        getDB >> Sql.execute parseFunction

    let executeNonQuery =
        getDB >> Sql.executeNonQuery

    let executeRow parseFunction =
        getDB >> Sql.executeRow parseFunction