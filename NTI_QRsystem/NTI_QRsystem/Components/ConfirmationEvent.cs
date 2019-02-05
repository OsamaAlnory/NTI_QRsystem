using System;
using System.Collections.Generic;
using System.Text;

namespace NTI_QRsystem.Components
{
    public interface ConfirmationEvent
    {
        bool OnAcceptButtonClicked();
        bool OnCancelButtonClicked();
    }
}
