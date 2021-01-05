import React from 'react';
import InputMask from 'react-input-mask';



const PhoneInputMask = ({ onChange, className, ...props }) => {
    const mask = <InputMask className={className} mask="+38\(999)9999999" maskChar=" " onChange={onChange} placeholder="Phone number"/>;
    return mask;
};

export default PhoneInputMask;