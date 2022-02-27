import React from "react";
import { useState, useEffect } from "react/cjs/react.development";
import ReviewForm from "../Components/ReviewForm";
import { render } from 'react-dom'
import { transitions, positions, Provider as AlertProvider } from 'react-alert'
import AlertTemplate from 'react-alert-template-basic'
import axios from "axios";
import Cookies from 'universal-cookie';

const Writing = (props) => {
    const [Loading, setLoading] = useState(false);
    const [ReviewApiData, setReviewApiData] = useState([]);
    const cookies = new Cookies();
    const token="Bearer "+ cookies.get('Token');
    const config = { headers: { 'Content-Type': 'multipart/form-data' ,'Authorization': token} };

    const options = {
        // you can also just use 'bottom center'
        position: positions.BOTTOM_CENTER,
        timeout: 5000,
        offset: '30px',
        // you can also just use 'scale'
        transition: transitions.SCALE
    }
    useEffect(() => {
        axios.get("https://localhost:44352/api/Review/GetReviewByUserId/" + props.id,config).then((response) => {
            getData(response.data)
        }).catch((err) => err.name);



    }, [])
    const getData = (data) => {
        setLoading(true)
        setReviewApiData(data)
        setTimeout(function () {
            setLoading(false)
        }, 1000);
    }
    const refresh = () => {
        console.log(props.id)
        axios.get("https://localhost:44352/api/Review/GetReviewByUserId/" + props.id,config)
            .then((response) => {
                getData(response.data)
            })
    }
    const onDelete = (id) => {
        axios.delete("https://localhost:44352/api/Review/" + id,config)
            .then(() => {
                refresh();
            })
    }

    if (props.email === undefined || props.email==="") {
        return (
            <div className="container">
                <div className="p-5 mt-5 shadow text-center">
                    <h1 className="text-warning display-1"><i className="bi bi-exclamation-triangle"></i></h1>
                    <hr />
                    <h4 className="text-secondary display-5">You need to sign In to post your review</h4>
                </div>
            </div>
        )
    }
    else {
        return (
            <div className="container">
                <div className="pt-3">
                    <div className="row">
                        <div className="col-lg-8 col-md-6 col-sm-12">
                            <div className="shadow p-3">
                                <h2 className="text-danger fw-bold">Welcome {props.email} to the dashboard</h2>
                                <p>Post your review on Gamehub</p>
                                <hr />
                                <AlertProvider template={AlertTemplate} position={positions.BOTTOM_RIGHT}>
                                    <ReviewForm userId={props.id} />
                                </AlertProvider>
                            </div>
                        </div>
                        <div className="col-lg-4 col-md-6 col-sm-12">
                            <div className="shadow p-3">
                                <div className="row">
                                    <div className="col-md-8">
                                        <h2 className="fw-bold">Your Last Review</h2>
                                        <small className="text-secondary">You can see your last 8 reviews that you've posted. Moreover you can delete each one of them</small>
                                    </div>
                                    <div className="col-md-4 text-end">
                                        <button onClick={() => refresh()} className="btn btn-lg display-1"> <i class="bi bi-arrow-repeat text-primary"></i></button>
                                    </div>
                                </div>
                                {Loading === true ?
                                    <div className="row">
                                        <div className="container">
                                            <div className="d-flex justify-content-center p-5">
                                                <div className="spinner-border text-info spinner-size" role="status">
                                                    <span className="visually-hidden">Loading...</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="d-flex justify-content-center p-5">
                                            Please wait
                                        </div>
                                    </div>
                                    :
                                    <div className="table-responsive lh-lg">
                                        <table className="table">
                                            <thead>
                                                <tr>
                                                    <th>Title</th>
                                                    <th>Category</th>
                                                    <td>Delete</td>
                                                </tr>
                                            </thead>
                                            <tbody>



                                                {ReviewApiData.map((data) => {
                                                    return (
                                                        <tr key={data.id}>
                                                            <td>{data.title}</td>
                                                            <td>{data.category.categoryName}</td>
                                                            <td><button className="btn btn-sm" onClick={() => onDelete(data.id)}><i class="bi bi-x text-danger"></i></button></td>
                                                        </tr>
                                                    )
                                                })
                                                }
                                            </tbody>
                                        </table>
                                    </div>
                                }</div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
};
export default Writing;