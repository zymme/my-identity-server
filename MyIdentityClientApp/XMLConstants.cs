using System;
namespace MyIdentityClientApp
{
    public static class XMLConstants
    {
        public const string SAML_RESPONSE = "<saml2p:Response xmlns:saml2p=\"urn:oasis:names:tc:SAML:2.0:protocol\"" +
                 " xmlns:saml2=\"urn:oasis:names:tc:SAML:2.0:assertion\"" +
                 " ID=\"_07f7d23303114d22820250370ae8ee75\"" +
                 " Version=\"2.0\"" +
                 " IssueInstant=\"2018-09-19T22:38:57Z\"" +
                 " Destination=\"https://peak10dev.service-now.com/navpage.do\"" +
                 " InResponseTo=\"SNCc483548e4da330edfe50365baab94111\">" +
                "<saml2:Issuer>http://localhost:5000</saml2:Issuer>" +
    "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\">" +
        "<SignedInfo> " +
           " <CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /> " +
           " <SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256\" /> " +
           " <Reference URI=\"#_07f7d23303114d22820250370ae8ee75\">" +
               " <Transforms>" +
                    "<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" />" +
                    "<Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />" +
                "</Transforms>" +
                "<DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#sha256\" />" +
                "<DigestValue>rhGdljPWXiOE083Qz9QmxTjA90xdqibLo/HOxhUW3Is=</DigestValue >" +
            "</Reference>" +
        "</SignedInfo>" +
        "<SignatureValue>j6mQsI93XsNpffizPKVWSE6P4txJRSDPedRDoKCApGi5RRQ+tkORErtAbO2SkIVL6J4i1COFZE9FuTF9o7DNRuhh6pUcHc/aW57oKfYV+zYnZCmTAWfPL1ITJ7labKkqTVS4NzwYOYBUeq6yMMwJFAe8tOpg1Dn6PB2ZjOzuUDPJNyrTY5GZNKkA/T3S9KnkAlMpjh2CjWFLVVftgOC3gk/X55Mbz0hIBswLG4nNvhYG1i7rhKUARQXQy63IBCaFQHzjce4tQEhOo+pqsoCuzyJv6ay6hjQOy2GlfZnzWQCpC1fR4KbxWN2twI0cOemE9ESA8hkgQS1jKIedH8YsWYCiZT5PYqfMqQwdsJtvvbZw7XigbbGW0oIyZ8OQ+ArXh+A8/2J/jl+TgpsrH8WUnDvOfE/oOKS25W23xGVMJkc8gXvA1lBitib6i/TMlteM6sZpY9gRZ9Xrv1xV2C20skOGpYAKMlxtmyVpRyBEU53DJvbLVyltNEnDI5lMZ8vLPYGTPyf8PsqqViLzOTMGYrb/9g/ImY+vC7HDgncQMk5vhexMVoasAMrcXxEzO3PLWnre0CsVcI6n7udH9NcjtQ4PxNkz5QWqmB11j9J76j3QNaY299BJ+Hoe35opSuLbidP+ST4zYlAOcLrvXH7nipUpGnxzeCTeldB5buNR+vc=</SignatureValue>" +
        "<KeyInfo>" +
            "<X509Data>" +
                "<X509Certificate>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</X509Certificate>" +
            "</X509Data>" +
        "</KeyInfo>" +
    "</Signature>" +
    "<saml2p:Status>" +
        "<saml2p:StatusCode Value=\"urn:oasis:names:tc:SAML:2.0:status:Success\" />" +
    "</saml2p:Status>" +
    "<saml2:Assertion ID=\"_3d630823-a872-4a69-b76b-64f40ffdd273\" Version=\"2.0\"" +
                     " IssueInstant=\"2018-09-19T22:38:57Z\">" +
        "<saml2:Issuer>http://localhost:5000</saml2:Issuer>" +
        "<saml2:Subject>" +
            "<saml2:NameID Format=\"urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified\">dave.zimmer@flexential.com</saml2:NameID>" +
            "<saml2:SubjectConfirmation Method=\"urn:oasis:names:tc:SAML:2.0:cm:bearer\">" +
                "<saml2:SubjectConfirmationData NotOnOrAfter=\"2018-09-19T22:40:57Z\"" +
                                               " InResponseTo=\"SNCc483548e4da330edfe50365baab94111\"" +
                                               " Recipient=\"https://peak10dev.service-now.com/navpage.do\" />" +
            "</saml2:SubjectConfirmation>" +
        "</saml2:Subject>" +
        "<saml2:Conditions NotBefore=\"2018-09-19T22:38:57Z\" NotOnOrAfter=\"2018-09-19T22:40:57Z\" >" +
            "<saml2:AudienceRestriction>" +
                "<saml2:Audience>https://peak10dev.service-now.com</saml2:Audience>" +
            "</saml2:AudienceRestriction>" +
        "</saml2:Conditions>" +
        "<saml2:AttributeStatement>" +
            "<saml2:Attribute Name=\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name\" >" +
                "<saml2:AttributeValue>Dave Zimmer</saml2:AttributeValue>" +
            "</saml2:Attribute>" +
        "</saml2:AttributeStatement>" +
        "<saml2:AuthnStatement AuthnInstant=\"2018-09-19T22:38:57Z\" SessionIndex=\"_3d630823-a872-4a69-b76b-64f40ffdd273\" >" +
             "<saml2:AuthnContext>" +
                "<saml2:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</saml2:AuthnContextClassRef>" +
             "</saml2:AuthnContext>" +
        "</saml2:AuthnStatement>" +
    "</saml2:Assertion>" +
"</saml2p:Response>";
    }
}
