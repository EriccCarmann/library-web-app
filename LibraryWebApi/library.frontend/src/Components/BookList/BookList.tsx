import React from "react";
import "./BookList.css";
import Book from "../Book/Book";

interface Props { };

const BookList: React.FC<Props> = (props: Props): JSX.Element => {
    return (
        <div>
            <Book
                cover="https://m.media-amazon.com/images/I/7125+5E40JL._AC_UF1000,1000_QL80_.jpg"
                title="Lord Of The Rings"
                genre="Adventure"
                description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse venenatis egestas mi, vitae euismod risus tristique in. Curabitur hendrerit varius lacinia."
                ISBN="459270689"
                author="Tolkien"
                available="Available"

            />
            <Book
                cover="https://m.media-amazon.com/images/I/7125+5E40JL._AC_UF1000,1000_QL80_.jpg"
                title="Hobbit"
                genre="Adventure"
                description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse venenatis egestas mi, vitae euismod risus tristique in. Curabitur hendrerit varius lacinia."
                ISBN="159230689"
                author="Tolkien"
                available="Not available"
            />
            <Book
                cover="https://m.media-amazon.com/images/I/7125+5E40JL._AC_UF1000,1000_QL80_.jpg"
                title="Guards! Guards!"
                genre="Fantasy"
                description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse venenatis egestas mi, vitae euismod risus tristique in. Curabitur hendrerit varius lacinia."
                ISBN="859020589"
                author="Terry Pratchett"
                available="Available"
            />
        </div>
    );
};

export default BookList;