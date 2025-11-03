<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireForm.aspx.cs" Inherits="Temiang.Avicenna.Module.Kiosk.QuestionnaireForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div>
        <%=RenderQuestion() %>
    </div>
    <script>
        $(document).ready(function () {
            <%=RenderScriptJS() %>
        });
        
    </script>
</body>
</html>
