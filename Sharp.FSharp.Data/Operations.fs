namespace Sharp.FSharp.Data

module Operations =
    let getConnectionString = 
        let ConnectionString = ConnectionString.GetSample()
        sprintf "Host=%s;Username=%s;Password=%s;Database=%s;sslmode=Require;Trust Server Certificate=true"
            ConnectionString.Host
            ConnectionString.Username
            ConnectionString.Password
            ConnectionString.Database
    
    