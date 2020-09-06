namespace Sharp.FSharp.Data

open Npgsql.FSharp
open Sharp.FSharp.Domain

module internal Operations =
    let getConnectionString = 
        let ConnectionString = ConnectionString.GetSample()
        sprintf "Host=%s;Username=%s;Password=%s;Database=%s;sslmode=Require;Trust Server Certificate=true"
            ConnectionString.Host
            ConnectionString.Username
            ConnectionString.Password
            ConnectionString.Database
    
    let private getDB queryString  = 
        getConnectionString
        |> Sql.connect
        |> Sql.query queryString

    let executeQuery func =
        getDB >> Sql.execute func

    let executeNonQuery =
        getDB >> Sql.executeNonQuery