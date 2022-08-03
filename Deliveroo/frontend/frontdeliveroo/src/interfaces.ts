export interface ICity {
    name: string;
    image: string;
}

export interface IUserInfo {
    userName: string;
    userEmail: string;
    userPassword: string;
    userPhoneNumber: string;
}


export interface IUserModel {
    userName: string,
    emailAddress: string,
    password: string,
    userPhoneNumber: string
}


export interface IAdress {
    city: string;
    postalCode: string;
    streetName: string;
}

export interface IUserProfile {
    userInfo: IUserInfo;
    userAddress: IAdress;
}

export interface IUserAccount {
    userName: string;
    userEmail: string;
    userPassword: string;
    userPhoneNumber: string;
    city: string;
    postalCode: string;
    streetName: string;
}