import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route } from 'react-router-dom';
import Cookies from 'universal-cookie';

export default function GetLastReviews() {
    const cookies = new Cookies();
    const token = "Bearer " + cookies.get('Token');
    const config = { headers: { 'Content-Type': 'application/json', 'Authorization': token } };
    const BaseImageUrl = "https://localhost:44352/ReviewImages/";
    const [GetLastReviews, setGetLastReviews] = useState([]);
    const [Loading, setLoading] = useState(true)
    
    useEffect(() => {
        axios.get("https://localhost:44352/api/review/getlast4reviews/").then((response) => {
            getData(response.data)
        }).catch((err) => err.name);

    }, [])
    const getData = (data) => {
        setLoading(true)
        setGetLastReviews(data)
        setTimeout(function () {
            setLoading(false)
        }, 1000);
    }

    const refreshGetLastReview = () => {
        getData();
    }
    return (
        <div>
            {
                Loading === true ?
                    <div className="text-center">
                        <h2>You can't have an access to the review</h2>
                        <h4>If you've already signed in, Click on refresh button</h4>
                    </div> :
                    <div className="row">
                        {
                            GetLastReviews.map((data) => {
                                return (

                                    <div className="col-md-3 " key={data.id}>
                                        <Link to={"/ReviewDetails/" + data.id} className="text-decoration-none link-dark">
                                            <div className="mb-4">
                                                <div className="pt-2 pb-2">
                                                    <img src={BaseImageUrl + data.image} className="rounded-3 img-fluid shadow-lg" />
                                                </div>
                                                <div>
                                                    <h4 className="pt-2 pb-2">
                                                        {data.title}
                                                    </h4>
                                                    <small><i className="bi bi-person"></i> {data.user.email}</small>
                                                </div>
                                                <button className={data.id % 2 === 0 ? "btn badge rounded-pill shadow bg-info" : "btn badge rounded-pill shadow bg-warning"}>{data.category.categoryName}</button>
                                            </div>
                                        </Link>
                                    </div>
                                )
                            })
                        }
                    </div>
            }
        </div>
    )
}