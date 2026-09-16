<%@ Page Title="Registra account di accesso esterno" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="AnagraficaWebForm.Account.RegisterExternalLogin" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <main>
        <h3>Esegui registrazione con account <%: ProviderName %> personale</h3>
        <asp:PlaceHolder runat="server">
            <h4>Form di associazione</h4>
            <hr />
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
            <p class="text-info">
                L'utente ha eseguito l'autenticazione con <strong><%: ProviderName %></strong>. Immettere di seguito un nome utente per il sito corrente
                e fare clic sul pulsante Accedi.
            </p>

            <div class="row">
                <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-2 col-form-label">Posta elettronica</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                        Display="Dynamic" CssClass="text-danger" ErrorMessage="Il campo Posta elettronica è obbligatorio" />
                    <asp:ModelErrorMessage runat="server" ModelStateKey="email" CssClass="text-error" />
                </div>
            </div>

            <div class="row">
                <div class="offset-md-2 col-md-10">
                    <asp:Button runat="server" Text="Accedi" CssClass="btn btn-outline-dark" OnClick="LogIn_Click" />
                </div>
            </div>
        </asp:PlaceHolder>
    </main>
</asp:Content>
