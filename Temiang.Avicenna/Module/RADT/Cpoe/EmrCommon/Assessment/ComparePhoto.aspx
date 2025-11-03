<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ComparePhoto.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ComparePhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 50%;">
                <fieldset>
                    <legend>Current Photo</legend>
                    <telerik:RadBinaryImage ID="imgPhoto" runat="server" AlternateText=""
                        Width="100%"
                        Height="100%" ResizeMode="None"
                        BorderStyle="Double"></telerik:RadBinaryImage>
                </fieldset>
            </td>
            <td>
                <fieldset>
                    <legend>Previouse Photo</legend>

                    <telerik:RadBinaryImage ID="imgPhotoPrev" runat="server" AlternateText=""
                        Width="100%"
                        Height="100%" ResizeMode="None"
                        BorderStyle="Double"></telerik:RadBinaryImage>
                </fieldset>
            </td>
        </tr>

    </table>

</asp:Content>
