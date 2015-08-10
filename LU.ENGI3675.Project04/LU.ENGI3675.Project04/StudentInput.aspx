<%@ Page Language="C#"  validateRequest="false"%>

<!DOCTYPE html>
<html>
<head>
    <title>
        Add New Students
    </title>
</head>
<body>
    <%
        if(Request.QueryString["Add"]=="TRUE")
        {
            try
            {
                LU.ENGI3675.Proj04.App_Code.DatabaseAccess.Create(Request.Form["fullName"], Request.Form["GPA"]);
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
                Response.Write("An Error occured. Please debug to see the cause.");
            }
        }      
    %>
    <form method="post">
        <table>
            <thead>
                <tr>
                    <th>SAFE adding:</th>
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
                    <td><input type="text" name="fullName" /></td>
                    <td><input type="text" name="GPA" /></td>
                    <td><input type="submit" formaction="StudentInput.aspx?Add=TRUE" value="Submit New Student" name="button"/></td>
                </tr>
            </tbody>
        </table>
    </form>
    <a href="StudentsTable.aspx">View Current Students UNSAFE</a>
    <a href="StudentsTable_XSS_SAFE.aspx">View Current Students SAFE</a>
    <a href="StudentInputUNSAFE.aspx"> New student entry UNSAFE</a>
</body>
</html>
