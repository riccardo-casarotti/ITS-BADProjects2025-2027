<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Anagrafica.aspx.cs" Inherits="AnagraficaWebForm.Anagrafica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestione Studenti</h2>

    

    <asp:SqlDataSource ID="sdsElencoStudenti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [Studente] WHERE [Matricola] = @Matricola" InsertCommand="INSERT INTO [Studente] ([Matricola], [Nome], [Cognome], [Email], [Classe]) VALUES (@Matricola, @Nome, @Cognome, @Email, @Classe)" SelectCommand="SELECT * FROM [Studente]" UpdateCommand="UPDATE [Studente] SET [Nome] = @Nome, [Cognome] = @Cognome, [Email] = @Email, [Classe] = @Classe WHERE [Matricola] = @Matricola">
        <DeleteParameters>
            <asp:Parameter Name="Matricola" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Matricola" Type="Int32" />
            <asp:Parameter Name="Nome" Type="String" />
            <asp:Parameter Name="Cognome" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Classe" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Nome" Type="String" />
            <asp:Parameter Name="Cognome" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Classe" Type="String" />
            <asp:Parameter Name="Matricola" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:HyperLink ID="NuovoStudente" runat="server" NavigateUrl="~/NuovoStudente">Nuovo Studente</asp:HyperLink>



    <asp:GridView ID="gvElencoStudenti" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Matricola" DataSourceID="sdsElencoStudenti" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" PageSize="50" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="Matricola" HeaderText="Matricola" ReadOnly="True" SortExpression="Matricola" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
            <asp:BoundField DataField="Cognome" HeaderText="Cognome" SortExpression="Cognome" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Classe" HeaderText="Classe" SortExpression="Classe" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>


</asp:Content>
