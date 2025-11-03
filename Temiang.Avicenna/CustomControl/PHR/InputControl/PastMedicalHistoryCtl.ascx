<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PastMedicalHistoryCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.PastMedicalHistoryCtl" %>
<telerik:RadGrid ID="grdMedicalHist" Width="400px" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                 GridLines="None" AllowMultiRowSelection="True" Skin="" HeaderStyle
                 OnItemDataBound="grdMedicalHist_ItemDataBound" OnNeedDataSource="grdMedicalHist_OnNeedDataSource">
    <MasterTableView DataKeyNames="ItemID" ShowHeader="False" ShowHeadersWhenNoRecords="false" Width="400px">
        <Columns>
            <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="30px">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkIsSelected"/>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Family Medical History" HeaderStyle-Width="120px" />
            <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes"  Display="False" />
            <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit" Display="True">
                <ItemTemplate>
                    <telerik:RadTextBox
                        ID="txtNotes" runat="server" ReadOnly='<%#IsReadMode %>'
                        Width="100%">
                    </telerik:RadTextBox>

                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <FilterMenu>
    </FilterMenu>
    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
        <Resizing AllowColumnResize="False" />
        <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
    </ClientSettings>
</telerik:RadGrid>
