<%@ Page Language="C#" validateRequest="false"%>

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
            List<LU.ENGI3675.Proj04.App_Code.Students> instance = LU.ENGI3675.Proj04.App_Code.DatabaseAccess.Read();
            foreach (LU.ENGI3675.Proj04.App_Code.Students temp in instance)
            {
                Response.Write("<tr>");
                Response.Write(string.Format("<td>{0}</td>",temp.Name));
                Response.Write(string.Format("<td>{0}</td>", temp.GPA));
                Response.Write("</tr>");
            }
        %>
            </table>
        <a href="StudentInput.aspx"> New student entry SAFE</a>
        <a href="StudentInputUNSAFE.aspx"> New student entry UNSAFE</a>
    </body>
</html>
