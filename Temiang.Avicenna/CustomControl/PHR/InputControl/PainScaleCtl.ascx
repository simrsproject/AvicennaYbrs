<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PainScaleCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.PainScaleCtl" %>

<%--Value base on -> QuestionAnswerSelectionID='LOC'--%>
<asp:RadioButtonList ID="optPainScale" runat="server" RepeatDirection="Horizontal" Width="400px">
    <asp:ListItem Value="00">0<img class="ps00"/></asp:ListItem>
    <asp:ListItem Value="01">1<img class="ps01"/></asp:ListItem>
    <asp:ListItem Value="02">2<img class="ps02"/></asp:ListItem>
    <asp:ListItem Value="03">3<img class="ps03"/></asp:ListItem>
    <asp:ListItem Value="04">4<img class="ps04"/></asp:ListItem>
    <asp:ListItem Value="05">5<img class="ps05"/></asp:ListItem>
    <asp:ListItem Value="06">6<img class="ps06"/></asp:ListItem>
    <asp:ListItem Value="07">7<img class="ps07"/></asp:ListItem>
    <asp:ListItem Value="08">8<img class="ps08"/></asp:ListItem>
    <asp:ListItem Value="09">9<img class="ps09"/></asp:ListItem>
    <asp:ListItem Value="10">10<img class="ps10"/></asp:ListItem>
</asp:RadioButtonList>