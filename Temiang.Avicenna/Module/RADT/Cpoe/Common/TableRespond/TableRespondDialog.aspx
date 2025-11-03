<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="TableRespondDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.TableRespondDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">
        <style type="text/css">
            .pretty-table { 
                border-collapse: collapse; 
                border: 1px solid #333;
                background: linear-gradient(white, antiquewhite);
            }
            .pretty-table th, .pretty-table td { 
                padding: 0.5em; 
                border: 1px dotted #666;
                color: #632a39; 
            }
            .pretty-table th[scope=col]
            {
                border-bottom: 2px solid #333;
                border-right: 2px solid #333;
                color: #000; background-color: #8fadcc;
            }
            .pretty-table th+th[scope=col] { 
                border-right: 1px dotted #666; 
                color: #fff; background-color: #7d98b3;
            }
            .pretty-table th[scope=row] { 
                border-right: 2px solid #333; 
                background-color: #b8cfe5;
            }
            .pretty-table tr.alt th, .pretty-table tr.alt td { 
                color: #2a4763; 
            }
            .pretty-table tr:hover th[scope=row], .pretty-table tr:hover td { 
                background-color: #632a2a;
                color: #fff;
            }
            .pretty-table caption
            {
                caption-side: bottom;
                font-size: 0.9em;
                font-style: italic;
                text-align: right;
                padding: 0.5em 0;
            }
        </style>
    </telerik:RadCodeBlock>

    <%=GenerateTable() %>
</asp:Content>
