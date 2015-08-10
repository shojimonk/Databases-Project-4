<%@ Page Language="C#"  validateRequest="false"%>

<!DOCTYPE html>
<html>
<head>
    <title></title>
</head>
<body>
    <%  
        if (Request.QueryString["AddUS"] == "TRUE")
        {

            try
            {
                LU.ENGI3675.Proj04.App_Code.DatabaseAccess.UnsafeCreate(Request.Form["fullNameUS"], Request.Form["GPAUS"]);
            }
            catch(Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
                Response.Write("An error occured. Please contact site Admin if the problem persists.");    
            }
            
        }
    %>
    <form method="post"> 
    <table>
            <thead>
                <tr>
                    <th>UNSAFE adding:</th>
                    <th></th>
                    <th></th>
                </tr>
                <tr>
                    <th> Student Full Name</th>
                    <th> Student GPA</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><input type="text" name="fullNameUS" /></td>
                    <td><input type="text" name="GPAUS" /></td>
                    <td><input type="submit" formaction="StudentInputUNSAFE.aspx?AddUS=TRUE" value="Submit New Student" name="button"/></td>
                </tr>
            </tbody>
        </table>
    </form>
    <a href="StudentsTable.aspx">View Current Students UNSAFE</a>
    <a href="StudentsTable_XSS_SAFE.aspx">View Current Students SAFE</a>
    <a href="StudentInput.aspx"> New student entry SAFE</a>
</body>
</html>
