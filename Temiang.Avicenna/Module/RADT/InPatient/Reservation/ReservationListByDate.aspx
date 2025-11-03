<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" 
	CodeBehind="ReservationListByDate.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.ReservationListByDate" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
            <MasterTableView DataKeyNames="Tanggal">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="Tanggal" HeaderText="Reservation Date" 
                         UniqueName="Tanggal" SortExpression="Tanggal" HeaderStyle-HorizontalAlign="Center" 
                         ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom1" HeaderText="BedId 01" 
                         UniqueName="Kolom1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom2" HeaderText="BedId 02" 
                         UniqueName="Kolom2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom3" HeaderText="BedId 03" 
                         UniqueName="Kolom3" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom4" HeaderText="BedId 04" 
                         UniqueName="Kolom4" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom5" HeaderText="BedId 05" 
                         UniqueName="Kolom5" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom6" HeaderText="BedId 06" 
                         UniqueName="Kolom6" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom7" HeaderText="BedId 07" 
                         UniqueName="Kolom7" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom8" HeaderText="BedId 08" 
                         UniqueName="Kolom8" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom9" HeaderText="BedId 09" 
                         UniqueName="Kolom9" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom10" HeaderText="BedId 10" 
                         UniqueName="Kolom10" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom11" HeaderText="BedId 11" 
                         UniqueName="Kolom11" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom12" HeaderText="BedId 12" 
                         UniqueName="Kolom12" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom13" HeaderText="BedId 13" 
                         UniqueName="Kolom13" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom14" HeaderText="BedId 14" 
                         UniqueName="Kolom14" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom15" HeaderText="BedId 15" 
                         UniqueName="Kolom15" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>  
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Kolom16" HeaderText="BedId 16" 
                         UniqueName="Kolom16" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>                                                                                                                            
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

