<%@ Page Language="C#" %>

<!DOCTYPE html>
<html>
    <head>
        <title>
            Current Students UNSAFE
        </title>
    </head>
    <body>
        <table>
            <thead>
                <tr>
                    <th>Student Name</th>
                    <th>GPA</th>
                </tr>
            </thead>
        <%
            //NEEDS TO BE CHANGED TO ESCAPE SPECIAL CHARACTERS
            List<LU.ENGI3675.Proj04.App_Code.Students> instance = LU.ENGI3675.Proj04.App_Code.DatabaseAccess.Read();
            foreach (LU.ENGI3675.Proj04.App_Code.Students temp in instance)
            {
                Response.Write("<tr><td>");
                Response.Write(HttpUtility.HtmlEncode(string.Format("{0}", temp.Name)));
                Response.Write("</td><td>");
                Response.Write(HttpUtility.HtmlEncode(string.Format("{0}", temp.GPA)));
                Response.Write("</td></tr>");
            }
        %>
            </table>
        <a href="StudentInput.aspx"> New student entry SAFE</a>
        <a href="StudentInputUNSAFE.aspx"> New student entry UNSAFE</a>
        <a href="StudentsTable.aspx">View Current Students UNSAFE</a>
    </body>
</html>
