<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionTemplate.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.PrescriptionSalesCommon.PrescriptionTemplate" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function showCopyPrescription(tno) {
            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            // retval pada event close windows
            var oArg = new Object();
            oArg.tno = tno; 
             
            //Close the RadWindow            
            oWnd.close(oArg);
        }
    </script>
    <telerik:RadGrid ID="grdPrescriptionTemplate" runat="server" CssClass="AutoHeightGridClass" EnableViewState="False"
    OnNeedDataSource="grdPrescriptionTemplate_NeedDataSource" OnDeleteCommand="grdPrescriptionTemplate_DeleteCommand" 
    AutoGenerateColumns="False" GridLines="None" Skin="" Width="360px" Height="360px">
    <MasterTableView DataKeyNames="TemplateNo" ShowHeader="False">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="TemplateNo" HeaderText="">
                <ItemStyle VerticalAlign="Top"></ItemStyle>
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "PrescriptionTemplate")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                ButtonType="ImageButton" ConfirmText="Delete this prescription template?">
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="False" UseStaticHeaders="True" />
    </ClientSettings>
</telerik:RadGrid>

</asp:Content>
